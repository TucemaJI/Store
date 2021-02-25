import { Component, OnInit } from '@angular/core';
import { IPageParameters } from 'src/app/modules/shared/models/IPageParameters';
import { Options } from "@angular-slider/ngx-slider";

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

  pageModel: any;
  pageParameters: IPageParameters;

  constructor() { }

  ngOnInit(): void {
    this.pageParameters = {
      itemsPerPage: 5,
      currentPage: 1,
      totalItems: 0,
    }
  }

  pageChanged(event) { }

}
