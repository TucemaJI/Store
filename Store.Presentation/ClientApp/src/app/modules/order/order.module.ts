import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OrdersComponent } from './components/orders/orders.component';
import { MaterialModule } from '../shared/material.module';
import { NgxPaginationModule } from 'ngx-pagination';
import { CurrencyPipe } from '../shared/services/currency.pipe';
import { PEPipe } from '../shared/services/pe.pipe';
import { StatusPipe } from '../shared/services/status.pipe';

@NgModule({
  declarations: [OrdersComponent, CurrencyPipe, PEPipe, StatusPipe],
  imports: [
    CommonModule,
    MaterialModule,
    NgxPaginationModule,
  ]
})

export class OrderModule { }
