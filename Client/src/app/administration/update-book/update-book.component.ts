import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Book } from 'src/app/models/book';
import { BookService } from 'src/app/services/book.service';
import { quantityLimitExceededValidator } from 'src/app/services/directives/quantity-limit.directive';

@Component({
  selector: 'app-update-book',
  templateUrl: './update-book.component.html',
  styleUrls: ['./update-book.component.css']
})

export class UpdateBookComponent implements OnInit {
  updateForm: FormGroup;
  book: Book | undefined;
  quantityField: number = 0;
  quantityLimitField: number = 0;

  constructor(
    private fb: FormBuilder,
    private bookService: BookService,
    private route: ActivatedRoute,
    private toastrService: ToastrService,
    private router: Router) {
    this.updateForm = this.fb.group({
      'description': ['', [Validators.required, Validators.minLength(3), Validators.maxLength(1000)]],
      'summaryDescription': ['', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]],
      'bookImage': ['', Validators.required],
      'price': ['', [Validators.required, Validators.min(0), Validators.max(100000000)]],
      'quantity': ['', [Validators.required, Validators.min(0), Validators.max(100000000)]],
      'quantityLimit': ['', [Validators.required, Validators.min(0), Validators.max(100000000)]],
    }, { validators: quantityLimitExceededValidator })
  }

  ngOnInit(): void {
    this.getBook();
  }

  getBook() {
    this.route.params.subscribe(params => {
      let id = params['id']
      this.bookService.details(id).subscribe(res => {
        this.book = res;
        this.updateForm = this.fb.group({
          'description': [`${this.book.description}`, [Validators.required, Validators.minLength(3), Validators.maxLength(1000)]],
          'summaryDescription': [`${this.book.summaryDescription}`, [Validators.required, Validators.minLength(3), Validators.maxLength(100)]],
          'bookImage': [`${this.book.bookImage}`, Validators.required],
          'price': [`${this.book.price}`, [Validators.required, Validators.min(0), Validators.max(100000000)]],
          'quantity': [`${this.book.quantity}`, [Validators.required, Validators.min(0), Validators.max(100000000)]],
          'quantityLimit': [`${this.book.quantityLimit}`, [Validators.required, Validators.min(0), Validators.max(100000000)]],
        })
      })
    })
  }

  update() {
    this.quantityField = +this.updateForm.value['quantity'];
    this.quantityLimitField = +this.updateForm.value['quantityLimit'];
    
    if (this.quantityLimitField < this.quantityField) {
      this.toastrService.error("The Qantity Limit should be More or Equal to the Qantity!");
    } else {
      let updateData = {
        'id': this.book?.id,
        'description': this.updateForm.value['description'],
        'summaryDescription': this.updateForm.value['summaryDescription'],
        'bookImage': this.updateForm.value['bookImage'],
        'price': this.updateForm.value['price'],
        'quantity': this.updateForm.value['quantity'],
        'quantityLimit': this.updateForm.value['quantityLimit']
      }
      this.bookService.update(updateData).subscribe(data => {
        this.toastrService.success("You have Updated a Book!");
        this.router.navigate([`/books/${this.book?.id}`])

      })
    }
  }

  get description() { return this.updateForm.get('description'); }
  get summaryDescription() { return this.updateForm.get('summaryDescription'); }
  get bookImage() { return this.updateForm.get('bookImage'); }
  get price() { return this.updateForm.get('price'); }
  get quantity() { return this.updateForm.get('quantity'); }
  get quantityLimit() { return this.updateForm.get('quantityLimit'); }
}

