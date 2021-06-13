import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { BookForList } from 'src/app/models/bookForList';
import { AuthService } from 'src/app/services/auth.service';
import { BookService } from 'src/app/services/book.service';

@Component({
  selector: 'app-search-books',
  templateUrl: './search-books.component.html',
  styleUrls: ['./search-books.component.css']
})
export class SearchBooksComponent implements OnInit {
  search: string;
  books: Array<BookForList> = [];
  searchedBooks: Array<BookForList> = [];


  constructor(
    private raute: ActivatedRoute,
    private bookService: BookService,) {
    this.search = "";
  }

  ngOnInit(): void {
    this.raute.params.subscribe(res => {
      this.search = res['search'];
      this.bookService.all().subscribe(res => {
        this.books = res;
        this.filterSerachedBooks()
      })
    })
  }

  filterSerachedBooks(){
    this.searchedBooks =[];
    if (this.search === "") {
      this.searchedBooks = this.books;
      console.log(this.searchedBooks);
    } else {
      this.books.forEach(book => {
        if (book.title.includes(this.search) || book.author.includes(this.search)) {
          this.searchedBooks.push(book);
        }
      })
    }
  }
}
