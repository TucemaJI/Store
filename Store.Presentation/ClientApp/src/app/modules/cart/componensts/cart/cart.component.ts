import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { BaseCartItem } from 'ng-shopping-cart';
import { ShoppingCartService } from 'src/app/modules/shared/services/shopping-cart.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {

  cartData: BaseCartItem[];
  displayedColumns: string[] = ['product', 'unitPrice', 'count', 'amount'];
  total: number = 0;

  constructor(public dialogRef: MatDialogRef<CartComponent>, private cartService: ShoppingCartService<BaseCartItem>) { }

  ngOnInit(): void {
    this.cartData = this.cartService.getItems();
    debugger;
    this.cartData.forEach(val => this.total += val.price * val.quantity);
  }
  update(item: BaseCartItem) {
    this.total = 0;
    debugger;
    this.cartService.addItem(item);
    this.cartData.forEach(val => this.total += val.price * val.quantity);
    debugger;
  }
  deleteProduct(item: BaseCartItem) {
    debugger;
    this.cartService.removeItem(item.id);
    this.cartData = this.cartService.getItems();
  }
  cancel(){
    this.dialogRef.close();
  }
  buy(){
    
  }
}
