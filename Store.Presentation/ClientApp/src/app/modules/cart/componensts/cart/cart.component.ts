import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { Store } from '@ngxs/store';
import { BaseCartItem } from 'ng-shopping-cart';
import { IOrderItem } from 'src/app/modules/shared/models/IOrderItem.model';
import { AuthService } from 'src/app/modules/shared/services/auth.service';
import { ShoppingCartService } from 'src/app/modules/shared/services/shopping-cart.service';
import { CreateOrder } from '../../store/cart.actions';
import { PaymentComponent } from '../payment/payment.component';
import { Consts } from 'src/app/modules/shared/consts';
import { ICreateOrder } from 'src/app/modules/shared/models/ICreateOrder.model';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {

  public cartData: BaseCartItem[];
  public displayedColumns: string[] = Consts.CART_COLUMNS;
  public total: number = 0;
  public userId: string;
  public orderCreated: boolean;
  public orderId: number;

  constructor(public dialogRef: MatDialogRef<CartComponent>, private cartService: ShoppingCartService<BaseCartItem>, private dialog: MatDialog, private store: Store,
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
    let isQuantityRight: boolean;
    this.cartData.forEach(i => {
      isQuantityRight = i.quantity < Consts.QUANTITY_MINIMUM_VALUE;
      if (isQuantityRight) {
        return;
      }
      description += `${i.name} count:${i.quantity} ,`,
        orderItems.push({
          printingEditionId: i.id, count: i.quantity, amount: i.quantity * i.price,
        })
    });
    if (isQuantityRight) {
      alert(Consts.QUANTITY_MINIMUM);
      return;
    }
    const order: ICreateOrder = {
      userId: this.userId,
      description: description,
      orderItemModels: orderItems,
    };
    this.store.dispatch(new CreateOrder(order));
    let dialogref: MatDialogRef<PaymentComponent>;
    this.store.subscribe(
      data => {
        if (data.cart.orderId != null && data.cart.orderStatus == null) {
          debugger;
          this.orderId = data.cart.orderId;
          debugger;
          this.orderCreated = true;
          dialogref = this.dialog.open(PaymentComponent, { data: { total: this.total, orderId: this.orderId, } });
          this.cartService.clean();
        }
        if (data.cart.orderStatus != null) {
          this.orderId = data.cart.orderId;
          this.orderCreated = data.cart.orderStatus;
        }
      }
    );
  }
}
