import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { BaseCartItem, CartService } from 'ng-shopping-cart';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {

  cartData: BaseCartItem[];
  displayedColumns: string[] = ['product', 'unitPrice', 'count', 'amount'];
  total: number = 0;

  constructor(public dialogRef: MatDialogRef<CartComponent>, private cartService: CartService<BaseCartItem>) { }

  ngOnInit(): void {
    this.cartData = this.cartService.getItems();
    this.cartData.forEach(val => this.total += val.total());
  }
  update() {
    this.total = 0;
    this.cartData.forEach(val => this.total += val.total());
    debugger;
  }
}
