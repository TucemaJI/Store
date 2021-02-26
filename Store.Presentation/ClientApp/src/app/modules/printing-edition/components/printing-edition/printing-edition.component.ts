import { Component, OnInit } from '@angular/core';
import { IPageParameters } from 'src/app/modules/shared/models/IPageParameters';
import { Options } from "@angular-slider/ngx-slider";

interface Curr {
  value: string;
  viewValue: string;
}
@Component({
  selector: 'app-printing-edition',
  templateUrl: './printing-edition.component.html',
  styleUrls: ['./printing-edition.component.css']
})

export class PrintingEditionComponent implements OnInit {

  value: number = 40;
  highValue: number = 60;
  options: Options = {
    floor: 0,
    ceil: 100
  };

  pEs= [
    {title: 'firstTitle', author: 'firstAuthor', price: 100},
    {title: 'secondTitle', author: 'secondAuthor', price: 200},
    {title: 'thirdTitle', author: 'thirdAuthor', price: 300},
    {title: 'fourthTitle', author: 'fourthAuthor', price: 400},
    {title: 'fifthTitle', author: 'fifthAuthor', price: 500},
    {title: 'sixTitle', author: 'sixAuthor', price: 600},
  ]

  currencies: Curr[] = [
    { value: 'usd-0', viewValue: "USD" },
    { value: 'uah-1', viewValue: "UAH" }
  ]

  pageModel: any;
  pageParameters: IPageParameters;

  constructor() { }

  ngOnInit(): void {
    this.pageParameters = {
      itemsPerPage: 6,
      currentPage: 1,
      totalItems: 0,
    }
  }

  pageChanged(event) { }

}
