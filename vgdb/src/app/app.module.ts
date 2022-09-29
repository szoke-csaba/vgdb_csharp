import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { NotFoundComponent } from './error-pages/not-found/not-found.component';
import { FooterComponent } from './_shared/footer/footer.component';
import { GameComponent } from './game/game.component';
import { GameSingleComponent } from './game/single/game-single.component';
import { HomeComponent } from './home/home.component';
import { MenuComponent } from './_shared/menu/menu.component';
import { ToastComponent } from './_shared/toast/toast.component';
import { AuthInterceptorProviders } from './_helpers/auth.interceptor';

const routes: Routes = [
  {
    path: '',
    title: 'Home | vgdb',
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
        title: 'Games | vgdb',
        component: GameComponent,
      },
      {
        path: ':id',
        title: 'Game Single | vgdb',
        component: GameSingleComponent
      }
    ]
  },
  {
    path: '404',
    title: '404 - Not Found | vgdb',
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
  providers: [AuthInterceptorProviders],
  bootstrap: [AppComponent]
})

export class AppModule { }
