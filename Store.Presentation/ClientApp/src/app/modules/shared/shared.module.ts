import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './header/header.component';
import { MaterialModule } from './material.module';
import { RouterModule } from '@angular/router';
import { ShoppingCartModule } from 'ng-shopping-cart';


@NgModule({
  declarations: [HeaderComponent, ],
  imports: [
    CommonModule,
    RouterModule,
    MaterialModule,
    ShoppingCartModule,
  ],
  exports: [HeaderComponent,]
})

export class SharedModule { }
