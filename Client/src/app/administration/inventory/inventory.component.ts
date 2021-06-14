import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BookForInventory } from 'src/app/models/bookForInventory';
import { BookService } from 'src/app/services/book.service';

@Component({
  selector: 'app-inventory',
  templateUrl: './inventory.component.html',
  styleUrls: ['./inventory.component.css']
})
export class InventoryComponent implements OnInit {
  books: Array<BookForInventory> = [];

  constructor(private bookService: BookService, private router: Router) { }

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
    console.log(id);
    this.router.navigate([`books/${id}`]);
  }
}