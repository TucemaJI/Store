import { BrowserModule } from '@angular/platform-browser';
import { InjectionToken, NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { StoreModule } from '@ngrx/store';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { environment } from '../environments/environment';
import { EffectsModule } from '@ngrx/effects';
import { StoreRouterConnectingModule } from '@ngrx/router-store';
import { AppEffects } from './store/effects/app.effects';

import { AccountModule } from './modules/account/account.module';
import { AdministratorModule } from './modules/administrator/administrator.module';
import { AuthorModule } from './modules/author/author.module';
import { CartModule } from './modules/cart/cart.module';
import { OrderModule } from './modules/order/order.module';
import { PrintingEditionModule } from './modules/printing-edition/printing-edition.module';
import { SharedModule } from './modules/shared/shared.module';
import { UserModule } from './modules/user/user.module';
import { MaterialModule } from './modules/shared/material.module';
import { ToastrModule, ToastNoAnimation, ToastNoAnimationModule } from 'ngx-toastr';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AccountHttpService } from './modules/shared/services/account-http.service';
import { AccountEffects } from './modules/account/store/account.effects';
import { appReducers } from './store/reducers/app.reducers';
import { TokenInterceptor } from './modules/shared/interceptors/auth.interceptor';
import { JwtHelperService, JwtModule } from '@auth0/angular-jwt';
import { AdministratorHttpService } from './modules/shared/services/administrator-http.service';
import { AdministratorEffects } from './modules/administrator/store/administrator.effects';
import { AuthService } from './modules/shared/services/auth.service';
import { ErrorInterceptor } from './modules/shared/interceptors/error.interceptor';
import { NgxPaginationModule } from 'ngx-pagination';
import { EnumToArray } from './modules/shared/services/enum-to-array';
import { PrintingEditionHttpService } from './modules/shared/services/printing-edition-http.service';
import { PrintingEditionEffects } from './modules/printing-edition/store/printing-edition.effects';
import { CartService, ShoppingCartModule } from 'ng-shopping-cart';
import { ShoppingCartService } from './modules/shared/services/shopping-cart.service';
import { CookieService } from 'ngx-cookie-service';

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    NgbModule,
    StoreModule.forRoot(appReducers),
    StoreDevtoolsModule.instrument({ maxAge: 25, logOnly: environment.production }),
    EffectsModule.forRoot([AppEffects, AccountEffects, AdministratorEffects, PrintingEditionEffects]),
    StoreRouterConnectingModule.forRoot(),
    ToastNoAnimationModule.forRoot(),
    NgxPaginationModule,
    ShoppingCartModule.forRoot(),

    MaterialModule,
    AccountModule,
    AdministratorModule,
    AuthorModule,
    CartModule,
    OrderModule,
    PrintingEditionModule,
    SharedModule,
    UserModule,

    HttpClientModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: () => {
          return localStorage.getItem('accessToken');
        }
      }
    }),
  ],
  providers: [AccountHttpService,
    AdministratorHttpService,
    AuthService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true,
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorInterceptor,
      multi: true,
    },
    PrintingEditionHttpService,
    ShoppingCartService,
    CookieService,

  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
