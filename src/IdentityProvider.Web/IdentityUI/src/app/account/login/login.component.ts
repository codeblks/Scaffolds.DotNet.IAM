import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AccountService, LoginContext } from '../account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  constructor(private route: ActivatedRoute, 
    private accountService: AccountService) { }

  ngOnInit() {
    this.route.queryParams
      .subscribe(params => {
        console.log(params.ReturnUrl);
        this.accountService
          .getLoginContext(params.ReturnUrl)
          .subscribe((context) => {
              console.log(context);
          });
      });
      // this.quoteService
      // .getRandomQuote({ category: 'dev' })
      // .pipe(
      //   finalize(() => {
      //     this.isLoading = false;
      //   })
      // )
      // .subscribe((quote: string) => {
      //   this.quote = quote;
      // });
    // this.route.queryParamMap.subscribe(params => {
    //   console.log('queryParamMap: ', params);
    // });
  }

  getCurrentYear() {
    return new Date().getFullYear();
  }
}
