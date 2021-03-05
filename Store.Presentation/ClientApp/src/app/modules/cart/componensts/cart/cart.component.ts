import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {

  cartData = [{product:'Product',price: 100,count: 1,amount: 100}];
  displayedColumns: string[] = ['product', 'unitPrice', 'count', 'amount'];

  constructor(public dialogRef: MatDialogRef<CartComponent>) { }

  ngOnInit(): void {
    
  }

}
