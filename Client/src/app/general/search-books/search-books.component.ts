import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { BookForList } from 'src/app/models/bookForList';
import { BookService } from 'src/app/services/book.service';

@Component({
  selector: 'app-search-books',
  templateUrl: './search-books.component.html',
  styleUrls: ['./search-books.component.css']
})
export class SearchBooksComponent implements OnInit {
  filterForm: FormGroup;
  search: string;
  books: Array<BookForList> = [];
  searchedBooks: Array<BookForList> = [];


  constructor(
    private raute: ActivatedRoute,
    private bookService: BookService,
    private fb: FormBuilder, 
    private toastrService: ToastrService) {
    this.search = "";
    this.filterForm = this.fb.group({
      'minValue': [''],
      'maxValue': [''],
    })
  }

  ngOnInit(): void {
    this.getbooks();
  }

  getbooks(){
    this.raute.params.subscribe(res => {
      this.search = res['search'];
      this.bookService.all().subscribe(books => {
        this.books = books;
        this.filterSerachedBooks()
        this.books = this.searchedBooks;
      })
    })
  }

  filterSerachedBooks() {
    this.searchedBooks = [];
    if (this.search === "") {
      this.searchedBooks = this.books;
    } else {
      this.books.forEach(book => {
        if (book.title.toLowerCase().includes(this.search.toLowerCase()) ||
           book.author.toLowerCase().includes(this.search.toLowerCase())) {
          this.searchedBooks.push(book);
        }
      })
    }
  }

  filter() {
    this.books = this.searchedBooks;
    let min = this.filterForm.value['minValue'];
    let max = this.filterForm.value['maxValue'];
    if(min < 0 || max < 0){
      this.toastrService.error("Max price and Min price mut be a postive numbers");
    }
    else if (min >= max) {
      this.toastrService.error("Your Max price should be more than the Min price");
    } else {
      this.toastrService.success(`Books from $ ${min} to $ ${max} prices.`);
      this.books = this.searchedBooks.filter(x => x.price >= min && x.price <= max);
      console.log(this.books);
      
    }
  }

  showAll() {
    this.getbooks();
    this.filterForm.reset();
    this.books = this.searchedBooks;
  }
}
