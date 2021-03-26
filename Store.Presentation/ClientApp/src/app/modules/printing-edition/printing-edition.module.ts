import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PrintingEditionComponent } from './components/printing-edition/printing-edition.component';
import { MaterialModule } from '../shared/material.module';
import { RouterModule } from '@angular/router';
import { NgxPaginationModule } from 'ngx-pagination';
import { NgxSliderModule } from '@angular-slider/ngx-slider';
import { SelectPEComponent } from './components/select-printing-edition/select-printing-edition.component';
import { ShoppingCartModule } from 'ng-shopping-cart';
import { PipesModule } from '../shared/pipes/pipes.module';

@NgModule({
  declarations: [PrintingEditionComponent, SelectPEComponent,],
  imports: [
    CommonModule,
    MaterialModule,
    RouterModule,
    NgxPaginationModule,
    NgxSliderModule,
    ShoppingCartModule,
    PipesModule.forRoot(),
  ],
  exports: [PrintingEditionComponent, SelectPEComponent,]
})

export class PrintingEditionModule { }
