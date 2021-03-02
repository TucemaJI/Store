import { Component, OnInit, SimpleChanges } from '@angular/core';
import { IPageParameters } from 'src/app/modules/shared/models/IPageParameters';
import { Options } from "@angular-slider/ngx-slider";
import { ECurrencyType } from 'src/app/modules/shared/models/ECurrencyType';
import { EnumToArray } from 'src/app/modules/shared/services/enum-to-array';
import { EPrintingEditionType } from 'src/app/modules/shared/models/EPrintingEditionType';
import { select, Store } from '@ngrx/store';
import { IAppState } from 'src/app/store/state/app.state';
import { selectPrintingEditions } from '../../store/printing-edition.selector';
import { IPrintingEdition } from 'src/app/modules/shared/models/IPrintingEdition';
import { getMaxPrice, getMaxPriceSuccess, getPE } from 'src/app/modules/printing-edition/store/printing-edition.actions';
import { PrintingEditionHttpService } from 'src/app/modules/shared/services/printing-edition-http.service';

@Component({
  selector: 'app-printing-edition',
  templateUrl: './printing-edition.component.html',
  styleUrls: ['./printing-edition.component.css']
})

export class PrintingEditionComponent implements OnInit {

  value: number = 0;
  highValue: number = 600;
  option: Options = {
    floor: 0,
    ceil: 0,
  }



  printingEditionsData: IPrintingEdition[];
  currencies = ECurrencyType;
  sortBy: string[] = [
    'Price: Low to High',
    'Price: High to Low',
  ]

  pageModel: any;
  pageParameters: IPageParameters;

  constructor(private store: Store<IAppState>,) {  }




  ngOnInit(): void {
    this.store.dispatch(getMaxPrice());
    this.store.pipe(select(selectPrintingEditions)).subscribe(
      data => {
        if (data.pageModel != null) {
          this.option = {
            floor: this.value,
            ceil: data.pageModel.maxPrice,
          };
        }
      }
    )

    this.pageParameters = {
      itemsPerPage: 6,
      currentPage: 1,
      totalItems: 0,
    }
    this.pageModel = {
      pageParameters: this.pageParameters,
      isDescending: false,
      orderByString: '',
      name: "",
      title: "",
      currency: 0,
      pEType: 0,
      minPrice: this.value,
      maxPrice: this.highValue,
    };
    this.store.dispatch(getPE(this.pageModel));
    this.getPrintingEditions();
  }

  test() {
    debugger;
    this.store.dispatch(getPE(this.pageModel));
  }

  getPrintingEditions() {
    this.store.pipe(select(selectPrintingEditions)).subscribe(

      data => {
        if (data.printingEditions != null && data.pageModel != null) {
          this.printingEditionsData = data.printingEditions;

          this.pageParameters = data.pageModel.pageParameters;
          console.log(data);
        }
      }
    )
  }
  sliderOptions(option: Options): Options {
    return {
      floor: option.floor,
      ceil: option.ceil,
    };
  }
  pageChanged(event) { }

}
