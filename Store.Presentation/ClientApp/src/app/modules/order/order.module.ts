import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OrdersComponent } from './components/orders/orders.component';
import { MaterialModule } from '../shared/material.module';
import { NgxPaginationModule } from 'ngx-pagination';
import { PipesModule } from '../shared/pipes/pipes.module';

@NgModule({
  declarations: [OrdersComponent,],
  imports: [
    CommonModule,
    MaterialModule,
    NgxPaginationModule,
    PipesModule.forRoot(),
  ]
})

export class OrderModule { }
