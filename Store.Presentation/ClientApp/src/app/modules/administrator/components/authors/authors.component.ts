import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { select, Store } from '@ngrx/store';
import { IAppState } from 'src/app/store/state/app.state';
import { IAuthor } from '../../../shared/models/IAuthor';
import { IPageParameters } from '../../../shared/models/IPageParameters';
import { getAuthors } from '../../store/administrator.actions';
import { selectAdministrator } from '../../store/administrator.selector';

@Component({
  selector: 'app-authors',
  templateUrl: './authors.component.html',
  styleUrls: ['./authors.component.css']
})
export class AuthorsComponent implements OnInit {

  displayedColumns: string[] = ['ID', 'Name', 'Product'];
  pageModel:any;
  pageParameters: IPageParameters;
  authorsData: IAuthor[];
  tempData: IAuthor;

  constructor(private store: Store<IAppState>, public dialog: MatDialog) { }

  ngOnInit(): void {
    this.pageParameters = {
      itemsPerPage: 5,
      currentPage: 1,
      totalItems: 0,
    }
    this.pageModel = {
      pageParameters: this.pageParameters,
      isDescending: false,
      orderByString: '',
      id: '',
      name: '',
    };
    this.store.dispatch(getAuthors(this.pageModel));
    
    // this.tempData = {
    //   id: 36,
    //   firstName: 'AuthorsName',
    //   secondName:'AuthorsSecondName',
    //   products: [
    //     "firstbook",
    //     'secondBook',
    //   ]
    // }
    // this.authorsData = [
    //   this.tempData,
    // ]
  }

  getAuthors() {
    this.store.pipe(select(selectAdministrator)).subscribe(

      data => {
        if (data.authors != null && data.pageModel != null) {
          this.authorsData = data.authors;

          this.pageParameters = data.pageModel.pageParameters;
          console.log(data);
        }
      }
    )
  }
  addAuthor() { }
  pageChanged(event) { }
  applyFilter() { }
  reverse() { }

}
