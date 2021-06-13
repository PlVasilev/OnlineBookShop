import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Book } from 'src/app/models/book';
import { AuthService } from 'src/app/services/auth.service';
import { BookService } from 'src/app/services/book.service';

@Component({
  selector: 'app-details-book',
  templateUrl: './details-book.component.html',
  styleUrls: ['./details-book.component.css']
})
export class DetailsBookComponent implements OnInit {
  id: string | undefined;
  book: Book | undefined;
 
  constructor(
    private raute: ActivatedRoute, 
    private bookService: BookService, 
    private authServise: AuthService,
    private router: Router,
    private toastrService: ToastrService) {
    this.raute.params.subscribe(res => {
      this.id = res['id'];
      this.bookService.details(this.id).subscribe(res => {
        this.book = res;
      })
    })
  }

  ngOnInit(): void {
  }

  get currentUser(){return this.authServise.isAutheticated()}

  get adminUser(){return this.authServise.isAdmin()}

  update(id: any){
    this.router.navigate(["/" + id + "/update"]);
  }

  delete(id: any){
    this.bookService.delete(id).subscribe(res => {
      this.toastrService.success("You have Deleted a Book!");
      this.router.navigate(["books"])
    }) 
  }

  buy(id: any){

  }
}
