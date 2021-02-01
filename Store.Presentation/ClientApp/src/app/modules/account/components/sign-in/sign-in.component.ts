import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { HttpService, Token, User } from '../../services/HttpService';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})
export class SignInComponent implements OnInit {
  userForm: FormGroup;

  token: Token;
  done: boolean = false;

  constructor(private router: Router, private httpService: HttpService) { }

  ngOnInit(): void {
    this.userForm = new FormGroup({
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required]),
    });
  }

  public hasError = (controlName: string, errorName: string) => {
    return this.userForm.controls[controlName].hasError(errorName);
  }

  public submit(userFormValue: User) {
    debugger;
    this.httpService.postData(userFormValue)
      .subscribe(
        (data: Token) => { this.token = data; this.done = true; debugger; },
        error => console.log(error)
      );
  }

}
