import { Component, OnInit } from '@angular/core';
import { IPageParameters } from 'src/app/modules/shared/models/IPageParameters';
import { Options } from "@angular-slider/ngx-slider";
import { ECurrencyType } from 'src/app/modules/shared/models/ECurrencyType';
import { select, Store } from '@ngrx/store';
import { IAppState } from 'src/app/store/state/app.state';
import { selectPrintingEditions } from '../../store/printing-edition.selector';
import { IPrintingEdition } from 'src/app/modules/shared/models/IPrintingEdition';
import { getPE } from 'src/app/modules/printing-edition/store/printing-edition.actions';
import { EPrintingEditionType } from 'src/app/modules/shared/models/EPrintingEditionType';

@Component({
  selector: 'app-printing-edition',
  templateUrl: './printing-edition.component.html',
  styleUrls: ['./printing-edition.component.css']
})

export class PrintingEditionComponent implements OnInit {

  lowValue: number = 0;
  highValue: number = 150;
  option: Options = {
    floor: 0,
    ceil: 0,
  }
  isDescending: boolean = false;
  searchText: string = '';

  selectedCurrency: string;
  selectedSort: string;

  bookBox: boolean = false;
  journalBox: boolean = false;
  newspaperBox: boolean = false;

  printingEditionsData: IPrintingEdition[];
  currencies = ECurrencyType;
  sortBy: string[] = [
    'Price: Low to High',
    'Price: High to Low',
  ]

  pageModel: any;
  pageParameters: IPageParameters;

  constructor(private store: Store<IAppState>,) { }

  ngOnInit(): void {
    this.pageParameters = {
      itemsPerPage: 6,
      currentPage: 1,
      totalItems: 0,
    }

    this.pageModel = {
      pageParameters: this.pageParameters,
      isDescending: this.isDescending,
      orderByString: '',
      name: "",
      title: "",
      currency: 0,
      pEType: [0],
      minPrice: this.lowValue,
      maxPrice: this.highValue,
    };
    this.store.dispatch(getPE(this.pageModel));
    this.getPrintingEditions();
  }

  getPrintingEditions() {
    this.store.pipe(select(selectPrintingEditions)).subscribe(
      data => {
        if (data.printingEditions != null && data.pageModel != null) {
          this.printingEditionsData = data.printingEditions;
debugger;
          this.option = {
            floor: data.pageModel.minPrice,
            ceil: data.pageModel.maxPrice,
          };
debugger;
          //this.highValue = data.pageModel.maxPrice;

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

  valueFilter(event){
    debugger;
    this.lowValue = event;
    this.applyFilter();
  }
  hValueFilter(event){
    debugger;
    this.highValue = event;
    this.applyFilter();
  }

  applyFilter() {
    this.isDescending = this.selectedSort === 'Price: High to Low' ? true : false;

    let pETypes: EPrintingEditionType[] = [];
    if (this.bookBox === true) {
      pETypes.push(EPrintingEditionType.book);
    }
    if (this.journalBox === true) {
      pETypes.push(EPrintingEditionType.journal);
    }
    if (this.journalBox === true) {
      pETypes.push(EPrintingEditionType.newspaper);
    }
    if (true !== this.bookBox !== this.journalBox !== this.newspaperBox) {
      pETypes = [0];
    }

    this.pageModel = {
      pageParameters: this.pageParameters,
      isDescending: this.isDescending,
      orderByString: 'Price',
      name: this.searchText,
      title: this.searchText,
      currency: this.currencies[this.selectedCurrency],
      pEType: pETypes,
      minPrice: this.lowValue,
      maxPrice: this.highValue,
    };
    debugger;
    this.store.dispatch(getPE(this.pageModel));
  }
}
