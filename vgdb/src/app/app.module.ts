import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule, Routes, TitleStrategy } from '@angular/router';
import { AppComponent } from './app.component';
import { NotFoundComponent } from './error-pages/not-found/not-found.component';
import { GameComponent } from './game/game.component';
import { GameSingleComponent } from './game/single/game-single.component';
import { HomeComponent } from './home/home.component';
import { AuthInterceptorProviders } from './_helpers/auth.interceptor';
import { CustomTitleStrategy } from './_helpers/custom-title-strategy';
import { FooterComponent } from './_shared/footer/footer.component';
import { MenuComponent } from './_shared/menu/menu.component';
import { ToastComponent } from './_shared/toast/toast.component';

const routes: Routes = [
  {
    path: '',
    title: 'Home',
    component: HomeComponent
  },
  {
    path: 'authentication',
    loadChildren: () =>
      import('./authentication/authentication.module')
      .then(m => m.AuthenticationModule)
  },
  {
    path: 'games',
    children: [
      {
        path: '',
        title: 'Games',
        component: GameComponent,
      },
      {
        path: ':id',
        component: GameSingleComponent
      }
    ]
  },
  {
    path: '404',
    title: '404 - Not Found',
    component: NotFoundComponent
  },
  {
    path: '**',
    redirectTo: '/404',
    pathMatch: 'full'
  }
];

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    MenuComponent,
    FooterComponent,
    NotFoundComponent,
    ToastComponent,
    GameComponent,
    GameSingleComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    RouterModule.forRoot(routes)
  ],
  providers: [AuthInterceptorProviders, { provide: TitleStrategy, useClass: CustomTitleStrategy }],
  bootstrap: [AppComponent]
})

export class AppModule { }
