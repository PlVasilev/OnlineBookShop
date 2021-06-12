import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BookService } from 'src/app/services/book.service';

@Component({
  selector: 'app-update-book',
  templateUrl: './update-book.component.html',
  styleUrls: ['./update-book.component.css']
})
export class UpdateBookComponent implements OnInit {

  updateForm: FormGroup;

  constructor(private fb: FormBuilder, private bookService: BookService) {
    this.updateForm = this.fb.group({
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
    console.log(this.updateForm.value);
    this.bookService.create(this.updateForm.value).subscribe(data => {
      console.log(data)
    })
  }
  get title() { return this.updateForm.get('title'); }
  get description() { return this.updateForm.get('description'); }
  get summaryDescription() { return this.updateForm.get('summaryDescription'); }
  get isbn() { return this.updateForm.get('isbn'); }
  get bookImage() { return this.updateForm.get('bookImage'); }
  get author() { return this.updateForm.get('author'); }
  get year() { return this.updateForm.get('year'); }
  get price() { return this.updateForm.get('price'); }
  get numberOfPages() { return this.updateForm.get('numberOfPages'); }
  get quantity() { return this.updateForm.get('quantity'); }
  get numberOfPurchases() { return this.updateForm.get('numberOfPurchases'); }

}