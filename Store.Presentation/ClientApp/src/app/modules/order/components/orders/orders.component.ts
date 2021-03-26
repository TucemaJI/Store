import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { select, Store } from '@ngrx/store';
import { PaymentComponent } from 'src/app/modules/cart/componensts/payment/payment.component';
import { EStatusType } from 'src/app/modules/shared/enums/status-type.enum';
import { IOrder } from 'src/app/modules/shared/models/IOrder.model';
import { IOrderPage } from 'src/app/modules/shared/models/IOrderPage.model';
import { IPageOptions } from 'src/app/modules/shared/models/IPageOptions.model';
import { AuthService } from 'src/app/modules/shared/services/auth.service';
import { IAppState } from 'src/app/store/state/app.state';
import { getOrders } from '../../store/order.action';
import { selectOrders } from '../../store/order.selector';
import { Consts } from 'src/app/modules/shared/consts';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css']
})
export class OrdersComponent implements OnInit {

  displayedColumns: string[] = Consts.ORDERS_COLUMNS;
  pageModel: IOrderPage;
  pageParameters: IPageOptions;
  ordersData: IOrder[];
  userId: string;

  constructor(private store: Store<IAppState>, public dialog: MatDialog, private auth: AuthService) { }

  ngOnInit(): void {
    this.userId = this.auth.getId();
    this.auth.userIdChanged.subscribe((id) => this.userId = id);
    this.pageParameters = Consts.ORDERS_PAGE_PARAMETERS;
    this.pageModel = {
      pageOptions: this.pageParameters,
      isDescending: false,
      orderByString: '',
      userId: this.userId,
      status: EStatusType.none,
    };
    this.store.dispatch(getOrders({ pageModel: this.pageModel }));
    this.getOrders();
  }

  getOrders(): void {
    this.store.pipe(select(selectOrders)).subscribe(

      data => {
        if (data.orders != null && data.pageModel != null) {
          this.ordersData = data.orders;
          this.pageParameters = data.pageModel.pageOptions;
        }
      }
    )
  }

  pageChanged(event: number): void {
    this.pageModel.pageOptions = { currentPage: event, itemsPerPage: this.pageParameters.itemsPerPage, totalItems: this.pageParameters.totalItems, };
    this.store.dispatch(getOrders({ pageModel: this.pageModel }));
  }

  pay(element: IOrder): void {
    this.dialog.open(PaymentComponent, { data: { total: element.totalAmount, orderId: element.id } });
  }
}
