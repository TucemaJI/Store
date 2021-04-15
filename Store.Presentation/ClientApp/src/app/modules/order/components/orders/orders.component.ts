import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Store } from '@ngxs/store';
import { PaymentComponent } from 'src/app/modules/cart/componensts/payment/payment.component';
import { EStatusType } from 'src/app/modules/shared/enums/status-type.enum';
import { IOrder } from 'src/app/modules/shared/models/IOrder.model';
import { IOrderPage } from 'src/app/modules/shared/models/IOrderPage.model';
import { IPageOptions } from 'src/app/modules/shared/models/IPageOptions.model';
import { AuthService } from 'src/app/modules/shared/services/auth.service';
import { GetOrders } from '../../store/order.action';
import { Consts } from 'src/app/modules/shared/consts';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css']
})
export class OrdersComponent implements OnInit {

  public displayedColumns: string[] = Consts.ORDERS_COLUMNS;
  public pageModel: IOrderPage = null;
  public pageOptions: IPageOptions;
  public ordersData: IOrder[];
  public userId: string;

  constructor(private store: Store, public dialog: MatDialog, private auth: AuthService) { }

  ngOnInit(): void {
    this.userId = this.auth.getId();
    this.auth.userIdChanged.subscribe((id) => this.userId = id);
    this.pageOptions = Consts.ORDERS_PAGE_PARAMETERS;
    this.pageModel = {
      pageOptions: this.pageOptions,
      isDescending: false,
      orderByString: '',
      userId: this.userId,
      status: EStatusType.none,
    };
    this.store.dispatch(new GetOrders(this.pageModel ));
    this.getOrders();
  }

  getOrders(): void {
    this.store.subscribe(
      data => {
        if (data.order.orders != null && data.order.pageModel != null) {
          this.ordersData = data.order.orders;
          this.pageOptions = data.order.pageModel.pageOptions;
        }
      }
    )
  }

  pageChanged(event: number): void {
    this.pageOptions = { currentPage: event, itemsPerPage: this.pageOptions.itemsPerPage, totalItems: this.pageOptions.totalItems, };
    const pageModel: IOrderPage = { ...this.pageModel, pageOptions: this.pageOptions };
    this.store.dispatch(new GetOrders( pageModel ));
  }

  pay(element: IOrder): void {
    this.dialog.open(PaymentComponent, { data: { total: element.totalAmount, orderId: element.id } });
    
    this.store.subscribe(
      data => {
        if (data.cart.orderStatus != null) {
          const pageModel: IOrderPage = { ...this.pageModel, pageOptions: this.pageOptions };
          this.store.dispatch(new GetOrders( pageModel ));
          element.status = data.cart.orderStatus;
        }
      }
    )
  }
}
