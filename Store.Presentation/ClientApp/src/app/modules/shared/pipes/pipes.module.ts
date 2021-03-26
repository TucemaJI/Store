import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { CurrencyPipe } from "./currency.pipe";
import { EnumToArrayPipe } from "./enum-to-array.pipe";
import { PrintiongEditionPipe } from "./printing-edition.pipe";
import { StatusPipe } from "./status.pipe";

@NgModule({
    imports: [CommonModule,],
    declarations: [ CurrencyPipe, EnumToArrayPipe, PrintiongEditionPipe, StatusPipe, ],
    exports: [ CurrencyPipe, EnumToArrayPipe, PrintiongEditionPipe, StatusPipe, ]
  })
  
  export class PipesModule {
    static forRoot(){
      return {
        ngModule: PipesModule,
        providers: []
      };
    }
  }