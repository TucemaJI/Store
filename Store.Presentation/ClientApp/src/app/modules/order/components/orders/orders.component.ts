import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { select, Store } from '@ngrx/store';
import { PaymentComponent } from 'src/app/modules/cart/componensts/payment/payment.component';
import { selectOrderId } from 'src/app/modules/cart/store/cart.selector';
import { EStatus } from 'src/app/modules/shared/models/EStatus';
import { IOrderModel } from 'src/app/modules/shared/models/IOrderModel';
import { IOrderPageModel } from 'src/app/modules/shared/models/IOrderPageModel';
import { IPageParameters } from 'src/app/modules/shared/models/IPageParameters';
import { AuthService } from 'src/app/modules/shared/services/auth.service';
import { IAppState } from 'src/app/store/state/app.state';
import { getOrders } from '../../store/order.action';
import { selectOrders } from '../../store/order.selector';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css']
})
export class OrdersComponent implements OnInit {

  displayedColumns: string[] = ['Order ID', 'Order time', 'Product', 'Title', 'Qty', 'Order amount', 'Order Status'];
  pageModel: IOrderPageModel;
  pageParameters: IPageParameters;
  ordersData: IOrderModel[];
  userId: string;

  constructor(private store: Store<IAppState>, public dialog: MatDialog, private auth: AuthService) { }

  ngOnInit(): void {
    debugger;
    this.userId = this.auth.getId();
    this.auth.userIdChanged.subscribe((id) => this.userId = id);
    this.pageParameters = {
      itemsPerPage: 5,
      currentPage: 1,
      totalItems: 0,
    }
    this.pageModel = {
      pageParameters: this.pageParameters,
      isDescending: false,
      orderByString: '',
      userId: this.userId,
      status: EStatus.none,
    };
    debugger;
    this.store.dispatch(getOrders({ pageModel: this.pageModel }));
    this.getOrders();
  }

  getOrders() {
    this.store.pipe(select(selectOrders)).subscribe(

      data => {
        if (data.orders != null && data.pageModel != null) {
          this.ordersData = data.orders;
          this.pageParameters = data.pageModel.pageParameters;
          console.log(data);
        }
      }
    )
  }
  pageChanged(event) {
    this.pageModel.pageParameters = { currentPage: event, itemsPerPage: this.pageParameters.itemsPerPage, totalItems: this.pageParameters.totalItems, };
    this.store.dispatch(getOrders({ pageModel: this.pageModel }));
  }
  pay(element: IOrderModel) {
    this.dialog.open(PaymentComponent, { data: { total: element.totalAmount, orderId: element.id } });

  }
}
