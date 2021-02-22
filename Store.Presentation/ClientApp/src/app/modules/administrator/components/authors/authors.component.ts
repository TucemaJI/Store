import { Component, OnInit } from '@angular/core';
import { IAuthor } from '../../models/IAuthor';
import { IPageParameters } from '../../models/IPageParameters';

@Component({
  selector: 'app-authors',
  templateUrl: './authors.component.html',
  styleUrls: ['./authors.component.css']
})
export class AuthorsComponent implements OnInit {

  displayedColumns: string[] = ['ID', 'Name', 'Product'];
  pageParameters: IPageParameters;
  authorsData: IAuthor[];

  constructor() { }

  ngOnInit(): void {
  }

  addAuthor(){}
  pageChanged(event){}

}
