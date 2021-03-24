import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CartComponent } from './componensts/cart/cart.component';
import { MaterialModule } from '../shared/material.module';
import { PaymentComponent } from './componensts/payment/payment.component';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [CartComponent, PaymentComponent],
  imports: [
    CommonModule,
    MaterialModule,
    RouterModule,
  ],
  exports: [CartComponent,],
})

export class CartModule { }
