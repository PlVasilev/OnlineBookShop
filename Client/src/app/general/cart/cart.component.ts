import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BookForCart } from 'src/app/models/bookForCart';
import { BookForCheckOut } from 'src/app/models/BookForCheckOut';
import { BookService } from 'src/app/services/book.service';
import { CartService } from 'src/app/services/cart.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {
  books: Array<BookForCart> = [];

  constructor(private cartSetrvice: CartService, private router: Router) { }

  ngOnInit(): void {
    this.getBooks();
  }

  getBooks(){
    this.cartSetrvice.getCart().subscribe(res => {
      this.books = res;
      console.log(this.books);
    })
  }
  checkOut(){
    let booksForChechOut: Array<BookForCheckOut> = [];
    booksForChechOut = this.books;
    this.cartSetrvice.checkOut(booksForChechOut).subscribe(res =>{
      console.log(res);
      
    })
  }
}
