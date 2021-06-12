import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BookForList } from 'src/app/models/bookForList';
import { BookService } from 'src/app/services/book.service';

@Component({
  selector: 'app-all-books',
  templateUrl: './all-books.component.html',
  styleUrls: ['./all-books.component.css']
})
export class AllBooksComponent implements OnInit {
  books: Array<BookForList> = [];

  constructor(private bookService: BookService, private router: Router) { }

  ngOnInit(): void {
    this.getCats();
  }
  getCats(){
    this.bookService.all().subscribe((books: BookForList[]) => {
      this.books = books;
      console.log(this.books);
    })
  }
 

  delete(id: any) {
    this.bookService.delete(id).subscribe(res => {
      console.log(res);
      this.getCats();
    })
  }

  edit(id: any) {
      this.router.navigate(["/contracts/" + id + "/edit"])
    } 

}