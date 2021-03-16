import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PrintingEditionComponent } from './components/printing-edition/printing-edition.component';
import { MaterialModule } from '../shared/material.module';
import { RouterModule } from '@angular/router';
import { NgxPaginationModule } from 'ngx-pagination';
import { NgxSliderModule } from '@angular-slider/ngx-slider';
import { EnumToArrayPipe } from '../shared/services/enum-to-array.pipe';
import { SelectPEComponent } from './components/select-pe/select-pe.component';
import { ShoppingCartModule } from 'ng-shopping-cart';



@NgModule({
  declarations: [PrintingEditionComponent, EnumToArrayPipe, SelectPEComponent,],
  imports: [
    CommonModule,
    MaterialModule,
    RouterModule,
    NgxPaginationModule,
    NgxSliderModule,
    ShoppingCartModule,
  ],
  exports: [PrintingEditionComponent, SelectPEComponent,]
})
export class PrintingEditionModule { }
