import { Injectable } from '@angular/core';
import { ThemeMatTheme, ThemeMatThemeType } from '../models/theme';

@Injectable({
  providedIn: 'root',
})
export class DigniteThemeService {
  themes: ThemeMatTheme[] = [];

  private defaultThemes: Array<ThemeMatTheme> = [
    {
      displayName: '默认主题',
      name: 'dignite-light',
      primaryColor: 'blue',
      addNameToBody: true,
      type: 'light',
    },
    {
      displayName: '默认主题',
      name: 'dignite-dark',
      primaryColor: 'blue',
      addNameToBody: true,
      type: 'dark',
    },
  ];

  constructor() {
    this.themes = [];
    this.addThemes(this.defaultThemes);
  }

  /**
   * 添加主题组
   * @param themes 主题
   */
  addThemes(themes: ThemeMatTheme[]) {
    for (const theme of themes) {
      this.addTheme(theme);
    }
  }

  /**
   * 主题
   * @param theme 主题
   */
  addTheme(theme: ThemeMatTheme): void {
    const index = this.themes.findIndex(m => m.name === theme.name);
    if (index === -1) {
      this.themes.push(theme);
    } else {
      this.themes[index] = { ...theme };
    }
  }

  getThemes(themeType?: ThemeMatThemeType) {
    if (themeType) {
      return this.themes.filter(m => m.type === themeType);
    } else {
      return this.themes;
    }
  }

  getTheme(themeName?: string) {
    const digniteTheme = this.themes.find(m => m.name === 'dignite-light');
    return themeName
      ? this.themes.find(m => m.name === themeName) || digniteTheme
      : this.themes.find(m => m.isDefault) || digniteTheme;
  }

  setTheme(themeName?: string) {
    const theme = this.getTheme(themeName);
    const body = document.querySelector('body');
    let element = document.querySelector<HTMLLinkElement>('#dignite-theme');
    if (theme.addNameToBody) {
      if (element) {
        element.remove();
      }
      body.setAttribute('theme', theme.name);
    } else {
      if (theme.styleHref) {
        body.removeAttribute('theme');
        if (element) {
          element.href = theme.styleHref;
        } else {
          element = document.createElement('link');
          element.rel = 'stylesheet';
          element.href = theme.styleHref;
          element.id = 'dignite-theme';
          document.querySelector('head').append(element);
        }
      }
    }
  }
}
