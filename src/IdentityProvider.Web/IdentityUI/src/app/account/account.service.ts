import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

const routes = {
  login: (returnUrl: string) => `api/account/login?ReturnUrl=${returnUrl}`
};

export interface LoginContext {
  username: string;
  password: string;
  rememberLogin: boolean;
  returnUrl: string;
  allowRememberLogin: boolean;
  enableLocalLogin: boolean;
  isExternalLoginOnly: boolean;
}

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  constructor(private httpClient: HttpClient) {}

  getLoginContext(context: string): Observable<LoginContext> {
    return this.httpClient
      //.cache()
      .get(routes.login(context))
      .pipe(
        map(result => <LoginContext>result),
        catchError(() => of('Error, could not get the login context'))
      );
  }
}
