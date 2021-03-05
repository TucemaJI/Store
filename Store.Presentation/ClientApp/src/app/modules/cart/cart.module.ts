import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CartComponent } from './componensts/cart/cart.component';
import { MaterialModule } from '../shared/material.module';



@NgModule({
  declarations: [CartComponent],
  imports: [
    CommonModule,
    MaterialModule,
  ],
  exports: [CartComponent,],
})
export class CartModule { }
