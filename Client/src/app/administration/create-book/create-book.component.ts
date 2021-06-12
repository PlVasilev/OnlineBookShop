import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { BookService } from 'src/app/services/book.service';

@Component({
  selector: 'app-create-book',
  templateUrl: './create-book.component.html',
  styleUrls: ['./create-book.component.css']
})
export class CreateBookComponent implements OnInit {

  bookForm: FormGroup;

  constructor(
    private fb: FormBuilder, 
    private bookService: BookService, 
    private router: Router,
    private toastrService: ToastrService) 
    {
    this.bookForm = this.fb.group({
      'title': ['', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]],
      'description': ['', [Validators.required, Validators.minLength(3), Validators.maxLength(1000)]],
      'summaryDescription': ['', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]],
      'isbn': ['', [Validators.required, Validators.pattern('[0-9-]{13}')]],
      'bookImage': ['', Validators.required],
      'author': ['', [Validators.required, Validators.minLength(3), Validators.maxLength(50)]],
      'year': ['', [Validators.required, Validators.min(0), Validators.max(3000)]],
      'price': ['', [Validators.required, Validators.min(0), Validators.max(100000000)]],
      'numberOfPages': ['', [Validators.required, Validators.min(1), Validators.max(100000000)]],
      'quantity': ['', [Validators.required, Validators.min(0), Validators.max(100000000)]],
      'numberOfPurchases': ['', [Validators.required, Validators.min(0), Validators.max(100000000)]],
    })

  }

  ngOnInit(): void {
  }

  create() {
    console.log(this.bookForm.value);
    this.bookService.create(this.bookForm.value).subscribe(data => {
      console.log(data);
      this.toastrService.success("success", "You have Created a Book!");
      this.router.navigate([`/books/${data['bookId']}`])
    })
  }

  get title() { return this.bookForm.get('title'); }
  get description() { return this.bookForm.get('description'); }
  get summaryDescription() { return this.bookForm.get('summaryDescription'); }
  get isbn() { return this.bookForm.get('isbn'); }
  get bookImage() { return this.bookForm.get('bookImage'); }
  get author() { return this.bookForm.get('author'); }
  get year() { return this.bookForm.get('year'); }
  get price() { return this.bookForm.get('price'); }
  get numberOfPages() { return this.bookForm.get('numberOfPages'); }
  get quantity() { return this.bookForm.get('quantity'); }
  get numberOfPurchases() { return this.bookForm.get('numberOfPurchases'); }

}
