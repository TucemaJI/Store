import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { select, Store } from '@ngrx/store';
import { BaseCartItem } from 'ng-shopping-cart';
import { IOrderItem } from 'src/app/modules/shared/models/IOrderItem.model';
import { IOrder } from 'src/app/modules/shared/models/IOrder.model';
import { AuthService } from 'src/app/modules/shared/services/auth.service';
import { ShoppingCartService } from 'src/app/modules/shared/services/shopping-cart.service';
import { IAppState } from 'src/app/store/state/app.state';
import { createOrder } from '../../store/cart.actions';
import { selectCartState } from '../../store/cart.selector';
import { PaymentComponent } from '../payment/payment.component';
import { Consts } from 'src/app/modules/shared/consts';
import { ICreateOrder } from 'src/app/modules/shared/models/ICreateOrder.model';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {

  cartData: BaseCartItem[];
  displayedColumns: string[] = Consts.CART_COLUMNS;
  total: number = 0;
  userId: string;
  orderCreated: boolean;
  orderId: number;

  constructor(public dialogRef: MatDialogRef<CartComponent>, private cartService: ShoppingCartService<BaseCartItem>, private dialog: MatDialog, private store: Store<IAppState>,
    private router: Router, private auth: AuthService) { }

  ngOnInit(): void {
    this.cartData = this.cartService.getItems();
    this.cartData.forEach(val => this.total += val.price * val.quantity);
    this.userId = this.auth.getId();
    this.dialogRef.afterClosed().subscribe(result => {
      if (this.orderCreated) {
        location.reload();
      };
    });
  }

  update(item: BaseCartItem): void {
    this.total = 0;
    this.cartService.addItem(item);
    this.cartData.forEach(val => this.total += val.price * val.quantity);
  }

  deleteProduct(item: BaseCartItem): void {
    this.cartService.removeItem(item.id);
    this.cartData = this.cartService.getItems();
  }

  cancel(): void {
    this.dialogRef.close();
  }

  buy(): void {
    if (this.userId === undefined) {
      this.router.navigateByUrl(Consts.ROUTE_SIGN_IN);
      return;
    }
    let description: string = '';
    let orderItems: IOrderItem[] = [];
    this.cartData.forEach(i => {
      if (i.quantity < Consts.QUANTITY_MINIMUM_VALUE) {
        alert(Consts.QUANTITY_MINIMUM)
        return;
      }
      description += `${i.name} count:${i.quantity} ,`,
        orderItems.push({
          printingEditionId: i.id, count: i.quantity, amount: i.quantity * i.price,
        })
    });
    const order: ICreateOrder = {
      userId: this.userId,
      description: description,
      orderItemModels: orderItems,
    };
    this.store.dispatch(createOrder({ order }));
    let dialogref: MatDialogRef<PaymentComponent>;
    this.store.pipe(select(selectCartState)).subscribe(
      data => {
        if (data.orderId != null && data.orderStatus == null) {
          this.orderId = data.orderId;
          this.orderCreated = true;
          dialogref = this.dialog.open(PaymentComponent, { data: { total: this.total, orderId: this.orderId, } });
          this.cartService.clean();
        }
        if (data.orderStatus != null) {
          this.orderId = data.orderId;
          this.orderCreated = data.orderStatus;
        }
      }
    );
  }
}
