import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { select, Store } from '@ngrx/store';
import { BaseCartItem } from 'ng-shopping-cart';
import { selectUser } from 'src/app/modules/printing-edition/store/printing-edition.selector';
import { IOrderItemModel } from 'src/app/modules/shared/models/IOrderItemModel';
import { IOrderModel } from 'src/app/modules/shared/models/IOrderModel';
import { ShoppingCartService } from 'src/app/modules/shared/services/shopping-cart.service';
import { IAppState } from 'src/app/store/state/app.state';
import { createOrder } from '../../store/cart.actions';
import { selectOrderId } from '../../store/cart.selector';
import { initialCartState } from '../../store/cart.state';
import { PaymentComponent } from '../payment/payment.component';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {

  cartData: BaseCartItem[];
  displayedColumns: string[] = ['product', 'unitPrice', 'count', 'amount'];
  total: number = 0;
  userId: string;

  constructor(public dialogRef: MatDialogRef<CartComponent>, private cartService: ShoppingCartService<BaseCartItem>, private dialog: MatDialog, private store: Store<IAppState>,
    private router: Router) { }

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
  cancel() {
    this.dialogRef.close();
  }
  buy() {
    if (this.userId === undefined) {
      this.router.navigateByUrl("sign-in");
    }
    let description: string = '';
    let orderItems: IOrderItemModel[] = [];
    this.cartData.forEach(i => { description += i.name, description += i.quantity, orderItems.push({ pEid: i.id, count: i.quantity, amount: i.quantity * i.price }) })
    const order: IOrderModel = {
      userId: this.userId,
      status: null,
      paymentId: null,
      description: description,
      orderItems: orderItems,
    };
    this.store.dispatch(createOrder({ order }));
    let orderId: number;
    this.store.pipe(select(selectOrderId)).subscribe(
      data => {
        if (data != null) {
          orderId = data;
        }
      }
    );

    if (orderId !== null) {
      const dialog = this.dialog.open(PaymentComponent, { data: { total: this.total, orderId: orderId } });
    }
  }
  selectUser() {
    this.store.pipe(select(selectUser)).subscribe(
      data => {
        if (data !== null) {
          debugger;
          this.userId = data.id;
        }
      }
    );
  }

}
