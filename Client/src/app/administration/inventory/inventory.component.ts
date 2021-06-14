import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { BookForInventory } from 'src/app/models/bookForInventory';
import { Inventory } from 'src/app/models/inventory';
import { BookService } from 'src/app/services/book.service';

@Component({
  selector: 'app-inventory',
  templateUrl: './inventory.component.html',
  styleUrls: ['./inventory.component.css']
})
export class InventoryComponent implements OnInit {
  books: Array<BookForInventory> = [];
  inventory!: Inventory;
  treshholdForm: FormGroup;
  quantityLimit: number = 0;
 

  constructor(private bookService: BookService, private router: Router, private fb: FormBuilder, private toastrService:ToastrService) { 
    this.treshholdForm = this.fb.group({
      'limit': ['', [Validators.required, Validators.min(0), Validators.max(100000000)]]
    })
  }

  ngOnInit(): void {
    this.getBooks();
  }

  getBooks(){
    this.bookService.inventory().subscribe((books: BookForInventory[]) => {
      console.log(books);
      this.books = books;
    })
  }

  update(id: string){
    this.router.navigate([`books/${id}`]);
  }

  setTreshhold(){  
    let quantityLimit = this.treshholdForm.value['limit'];
    this.bookService.updateQantityLimit(this.treshholdForm.value).subscribe(res =>{
      this.toastrService.success(`You have updated the Quantity minimum limit to ${quantityLimit} - Books with wantity below ${quantityLimit} will be marked is LIMITED`);
      this.getBooks();
      this.treshholdForm.reset()
    })
  }

  get limit() {return this.treshholdForm.get('limit'); }
}