import { Component, OnInit } from '@angular/core';
import { IPageParameters } from 'src/app/modules/shared/models/IPageParameters.model';
import { Options } from "@angular-slider/ngx-slider";
import { ECurrencyType } from 'src/app/modules/shared/models/currency-type.enum';
import { select, Store } from '@ngrx/store';
import { IAppState } from 'src/app/store/state/app.state';
import { selectPrintingEditions } from '../../store/printing-edition.selector';
import { IPrintingEdition } from 'src/app/modules/shared/models/IPrintingEdition.model';
import { getPEs } from 'src/app/modules/printing-edition/store/printing-edition.actions';
import { EPrintingEditionType } from 'src/app/modules/shared/models/printing-edition-type.enum';
import { IPrintingEditionPage } from 'src/app/modules/shared/models/IPEPage.model';
import { Consts } from 'src/app/modules/shared/consts';

@Component({
  selector: 'app-printing-edition',
  templateUrl: './printing-edition.component.html',
  styleUrls: ['./printing-edition.component.css']
})

export class PrintingEditionComponent implements OnInit {

  lowValue: number = 0;
  highValue: number = 0;
  option: Options = Consts.PE_OPTIONS;
  isDescending: boolean = false;
  searchText: string = '';

  selectedCurrency: string = ECurrencyType[1];
  selectedSort: string;

  bookBox: boolean = false;
  journalBox: boolean = false;
  newspaperBox: boolean = false;

  printingEditionsData: IPrintingEdition[];
  currencies = ECurrencyType;
  sortBy: string[] = [
    Consts.SORT_LOW_TO_HIGH,
    Consts.SORT_HIGH_TO_LOW,
  ]

  pageModel: IPrintingEditionPage;
  pageParameters: IPageParameters;

  constructor(private store: Store<IAppState>) { }

  ngOnInit(): void {
    this.pageParameters = Consts.PE_PAGE_PARAMETERS;
    this.pageModel = {
      pageParameters: this.pageParameters,
      isDescending: this.isDescending,
      orderByString: '',
      name: "",
      title: "",
      currency: 0,
      pEType: [0],
      minPrice: this.lowValue,
      maxPrice: Number.MAX_SAFE_INTEGER,
    };
    this.store.dispatch(getPEs({ pageModel: this.pageModel }));
    this.getPrintingEditions();
  }

  getPrintingEditions(): void {
    this.store.pipe(select(selectPrintingEditions)).subscribe(
      data => {
        if (data.printingEditions != null && data.pageModel != null) {
          this.printingEditionsData = data.printingEditions;
          this.option = {
            floor: data.pageModel.minPrice,
            ceil: data.pageModel.maxPrice,
          };
          if (this.highValue < 1) {
            this.highValue = data.pageModel.maxPrice;
          }
          this.pageParameters = data.pageModel.pageParameters;
        }
      }
    );
  }

  pageChanged(event: number): void {
    this.pageModel.pageParameters = { currentPage: event, itemsPerPage: this.pageParameters.itemsPerPage, totalItems: this.pageParameters.totalItems, };
    this.store.dispatch(getPEs({ pageModel: this.pageModel }));
  }

  applyFilter(): void {
    this.isDescending = this.selectedSort === Consts.SORT_HIGH_TO_LOW ? true : false;

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
      orderByString: Consts.SORT_BY,
      name: this.searchText,
      title: this.searchText,
      currency: this.currencies[this.selectedCurrency],
      pEType: pETypes,
      minPrice: this.lowValue,
      maxPrice: this.highValue,
    };
    this.store.dispatch(getPEs({ pageModel: this.pageModel }));
  }
}
