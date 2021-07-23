import { Injectable } from '@angular/core';
import { ThemeMatTheme, ThemeMatThemeType } from '../tokens/theme-basic.token';

@Injectable({
    providedIn: 'root'
})

export class DigniteThemeService {

    themes: ThemeMatTheme[] = [];

    private defaultThemes: Array<ThemeMatTheme> = [{
        displayName: '默认主题',
        name: 'default',
        primaryColor: 'red',
        addNameToBody: true,
        type: 'light',
    }];

    constructor(
    ) {
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

    getTheme(themeName: string) {
        return this.themes.find(m => m.name === themeName);
    }

    setTheme(themeName: string) {
        const theme = this.getTheme(themeName);
        let element = document.querySelector<HTMLLinkElement>('#dignite-theme');
        if (theme.addNameToBody) {
            if (element) {
                element.remove();
            }
            document.querySelector('body').setAttribute('theme', theme.name);
        } else {
            if (theme.styleHref) {
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
