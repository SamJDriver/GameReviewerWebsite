import React, { useState } from 'react'
import { Link, router } from "expo-router";
import { Image, StyleSheet, Platform, TouchableOpacity, View } from 'react-native';
import { PaperProvider, TextInput, Button, Text, MD3DarkTheme } from 'react-native-paper';
import { HelloWave } from '@/components/HelloWave';
import ParallaxScrollView from '@/components/ParallaxScrollView';
import { ThemedText } from '@/components/ThemedText';
import { ThemedView } from '@/components/ThemedView';

// This is the entry point of the app

export default function LoginScreen() {
  const [email, setEmail] = useState({ value: '', error: '' })
  const [password, setPassword] = useState({ value: '', error: '' })

  const onLoginPressed = () => {
    // TODO implement. Going off of https://github.com/venits/react-native-login-template/blob/master/src/screens/LoginScreen.js 
    // const emailError = emailValidator(email.value)
    // const passwordError = passwordValidator(password.value)
    // if (emailError || passwordError) {
    //   setEmail({ ...email, error: emailError })
    //   setPassword({ ...password, error: passwordError })
    //   return
    // }
    // navigation.reset({
    //   index: 0,
    //   routes: [{ name: 'Dashboard' }],
    // })
    router.replace('home/(tabs)/')
  
  }

  return (
    <View style={styles.bigContainer}>
      <View style={styles.margins}>

        {/* TODO logo image here */}
        <Image style={{ justifyContent: 'center', alignSelf: 'center' }} source={require('./../assets/images/react-logo.png')} />

        <View style={styles.loginContainer}>
          <Text>Login to continue</Text>

          {/* TODO: be able to login with email OR username? pie in the sky*/}
          <TextInput
            label="Email"
            value={email.value}
            onChangeText={(text) => setEmail({ value: text, error: '' })}
            autoCorrect={false}
            autoCapitalize='none'
            keyboardType='email-address'
            autoComplete='email'
            textContentType="emailAddress"
            error={!!email.error}
            style={styles.textInputStyle}
          />

          <TextInput
            label="Password"
            returnKeyType="done"
            value={password.value}
            onChangeText={(text) => setPassword({ value: text, error: '' })}
            secureTextEntry
            error={!!password.error}
            style={styles.textInputStyle}
          />

          <Button mode="contained" buttonColor='#0d99ff' textColor='white' onPress={onLoginPressed}>
            <Text style={styles.buttonText}>Sign in</Text>
          </Button>
        </View>

        <Button mode="outlined" onPress={() => router.navigate('RegisterScreen')}>
          <Text style={styles.buttonText}>Sign up</Text>
        </Button>

        <Button onPress={() => router.navigate('ForgotPasswordScreen')}>
          <Text style={styles.buttonText}>Forgot your password?</Text>
        </Button>

      </View>
    </View>
  );
}

const styles = StyleSheet.create({
  margins: {
    marginHorizontal: "5%",
    marginVertical: "5%",
    justifyContent: "space-evenly",
    flex: 1
  },
  bigContainer: {
    backgroundColor: '#101316',
    flex: 1,

  },
  loginContainer: {
    marginVertical: "2%",
    gap: 10,
  },
  reactLogo: {
    height: 178,
    width: 290,
    bottom: 0,
    left: 0,
    position: 'absolute',
  },
  forgotPassword: {
    width: '100%',
    alignItems: 'flex-end',
    marginBottom: 24,
  },
  row: {
    flexDirection: 'row',
    marginTop: 4,
  },
  forgot: {
    fontSize: 13,
    color: MD3DarkTheme.colors.secondary,
  },
  buttonText: {
    fontWeight: 'bold',
    color: 'white',
  },
  textInputStyle: {
    selectionColor: 'white',
    cursorColor: 'white',
    underlineColor: 'white',
    activeUnderlineColor: 'white',
  }
});
