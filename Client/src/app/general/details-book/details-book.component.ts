import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Book } from 'src/app/models/book';
import { AuthService } from 'src/app/services/auth.service';
import { BookService } from 'src/app/services/book.service';
import { CartService } from 'src/app/services/cart.service';


@Component({
  selector: 'app-details-book',
  templateUrl: './details-book.component.html',
  styleUrls: ['./details-book.component.css']
})
export class DetailsBookComponent implements OnInit {
  id: string | undefined;
  book: Book | undefined;
  buyForm: FormGroup;
 
  constructor(
    private fb: FormBuilder,
    private raute: ActivatedRoute, 
    private bookService: BookService, 
    private cartService: CartService,
    private authServise: AuthService,
    private router: Router,
    private toastrService: ToastrService) {
    this.getbook();
    this.buyForm = this.fb.group({
      'quantity': ['', [Validators.required, Validators.min(0), Validators.max(100000000)]],
    })
  };
  

  ngOnInit(): void {
  }

  get currentUser(){return this.authServise.isAutheticated()}

  get adminUser(){return this.authServise.isAdmin()}

  get quantity() {
    return this.buyForm.get('quantity');
  }

  getbook(){
    this.raute.params.subscribe(res => {
      this.id = res['id'];
      this.bookService.details(this.id).subscribe(res => {
        this.book =  res;
      })
    })
  }

  update(id: any){
    this.router.navigate(["/" + id + "/update"]);
  }

  delete(id: any){
    this.bookService.delete(id).subscribe(res => {
      this.toastrService.success("You have Deleted a Book!");
      this.router.navigate(["books"])
    }) 
  }

  addToCart(){
    let selectedQuantity = this.buyForm.value['quantity'];
    if(this.book !== undefined && selectedQuantity > this.book.quantity){
      this.toastrService.error(`You Can NOT select more than ${this.book.quantity} copies !`);
    } else{
      let data = {
        "bookId": this.book?.id,
        "quantity": selectedQuantity
      }
      this.cartService.addToCart(data).subscribe(res =>{
        this.router.navigate(["books"])
      })
    }
  }
}
