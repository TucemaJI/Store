import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './header/header.component';
import { StoreModule } from '@ngrx/store';
import { sharedReducer, sharedReducerNode } from './store/shared.reducer';
import { MaterialModule } from './material.module';
import { RouterModule } from '@angular/router';
import { EnumToArrayPipe } from './services/enum-to-array.pipe';
import { ShoppingCartModule } from 'ng-shopping-cart';


@NgModule({
  declarations: [HeaderComponent, ],
  imports: [
    CommonModule,
    StoreModule.forFeature(sharedReducerNode, sharedReducer),
    RouterModule,
    MaterialModule,
    ShoppingCartModule,
  ],
  exports: [HeaderComponent,]
})
export class SharedModule { }
