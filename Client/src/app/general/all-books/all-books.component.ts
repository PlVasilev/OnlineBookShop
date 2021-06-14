import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { BookForList } from 'src/app/models/bookForList';
import { BookService } from 'src/app/services/book.service';

@Component({
  selector: 'app-all-books',
  templateUrl: './all-books.component.html',
  styleUrls: ['./all-books.component.css']
})
export class AllBooksComponent implements OnInit {
  books: Array<BookForList> = [];
  databaseBooks: Array<BookForList> = [];

  filterForm: FormGroup;

  constructor(private fb: FormBuilder, private bookService: BookService, private toastrService: ToastrService) {
    this.filterForm = this.fb.group({
      'minValue': [''],
      'maxValue': [''],
    })
  }

  ngOnInit(): void {
    this.getBooks();
  }

  getBooks() {
    this.bookService.all().subscribe((books: BookForList[]) => {
      this.databaseBooks = books;
      this.books = books;
    })
  }

  get minValue() {
    return this.filterForm.get('minValue');
  }

  get maxValue() {
    return this.filterForm.get('maxValue');
  }

  filter() {
    this.books = this.databaseBooks;
    let min = this.filterForm.value['minValue'];
    let max = this.filterForm.value['maxValue'];
    if (min >= max) {
      this.toastrService.error("You have Updated a Book!");
    } else {
      this.toastrService.success(`Books from $ ${min} to $ ${max} prices.`);
      this.books = this.databaseBooks.filter(x => x.price >= min && x.price <= max);
    }
  }

  showAll(){
    this.books = this.databaseBooks;
  }
}
