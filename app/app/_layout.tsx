import { Stack } from "expo-router";
import { useColorScheme } from "react-native";
import { MD3DarkTheme, PaperProvider, ThemeProvider } from "react-native-paper";

export default function RootLayout() {
  const colorScheme = useColorScheme()
  return (
    <PaperProvider>
      <ThemeProvider theme={MD3DarkTheme}>
        <Stack>
          <Stack.Screen name="index" />
        </Stack>
      </ThemeProvider>
    </PaperProvider>
  );
}
