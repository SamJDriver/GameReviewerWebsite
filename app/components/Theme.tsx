
import {
  MD3DarkTheme as DefaultTheme,
} from 'react-native-paper';

const theme = {
  ...DefaultTheme,
  // Specify custom property
  //  myOwnProperty: true,
  // Specify custom property in nested object
  colors: {
    ...DefaultTheme.colors,
    primaryColor: '#1E1E1E',
    secondaryColor: '#000000',
    tertiaryColor: '0d99ff', 
    // tertiaryColor: some sort of purple?
  },
};
