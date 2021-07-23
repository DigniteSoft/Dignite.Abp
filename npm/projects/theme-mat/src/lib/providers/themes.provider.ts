import { APP_INITIALIZER, Injector } from '@angular/core';
import { ThemeMatOptions } from '../models/theme';
import { DigniteThemeService } from '../services/theme.service';
import { InjectionToken } from '@angular/core';

export const THEME_MAT_OPTIONS = new InjectionToken('THEME_MAT_OPTIONS');

export const THEME_MAT_THEMES_PROVIDERS = [
    {
        provide: APP_INITIALIZER,
        deps: [Injector],
        useFactory: configureThemes,
        multi: true,
    },
];

export function configureThemes(injector: Injector) {
    return () => {
        const themeService = injector.get<DigniteThemeService>(DigniteThemeService);
        const { themes } = injector.get<ThemeMatOptions>(THEME_MAT_OPTIONS);
        if (themes && themes.length) {
            themeService.addThemes(themes);
            themeService.setTheme();
        }
    };
}

