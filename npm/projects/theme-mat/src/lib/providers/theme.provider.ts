import { Injector } from '@angular/core';
import { DigniteThemeService } from '../services/theme.service';
import { ThemeMatOptions, THEME_MAT_OPTIONS } from '../tokens/theme-basic.token';

export function themeFactory(injector: Injector) {
    const themeService = injector.get<DigniteThemeService>(DigniteThemeService);
    const { themes } = injector.get<ThemeMatOptions>(THEME_MAT_OPTIONS);
    if (themes && themes.length) {
        themeService.addThemes(themes);
    }
    return () => { };
}

