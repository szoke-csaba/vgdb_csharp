import { Injectable } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { RouterStateSnapshot, TitleStrategy } from '@angular/router';

@Injectable({ providedIn: 'root' })
export class CustomTitleStrategy extends TitleStrategy {
  constructor(private readonly title: Title) {
    super();
  }

  override updateTitle(routerStateSnapshot: RouterStateSnapshot): void {
    const title = this.buildTitle(routerStateSnapshot);

    if (title !== undefined) {
      this.title.setTitle(`${title} | vgdb`);
    } else {
      this.title.setTitle(`vgdb`);
    }
  }
}
