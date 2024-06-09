import React, { useState } from 'react'
import { Link, router } from "expo-router";
import { View, StyleSheet, TouchableOpacity } from 'react-native'
import { PaperProvider, TextInput, Button, Text, MD3DarkTheme } from 'react-native-paper';
import UploadImage from '../components/UploadImage';
// import Background from '../components/Background'
// import Logo from '../components/Logo'
// import Header from '../components/Header'
// import Button from '../components/Button'
// import TextInput from '../components/TextInput'
// import BackButton from '../components/BackButton'
// import { theme } from '../core/theme'
// import { emailValidator } from '../helpers/emailValidator'
// import { passwordValidator } from '../helpers/passwordValidator'
// import { nameValidator } from '../helpers/nameValidator'

// TODO copy pasted literally eveverything from here: https://github.com/venits/react-native-login-template/blob/master/src/screens/RegisterScreen.js, fix it

export default function RegisterScreen() {
  const [username, setUsername] = useState({ value: '', error: '' })
  const [email, setEmail] = useState({ value: '', error: '' })
  const [password, setPassword] = useState({ value: '', error: '' })
  const [passwordConfirmation, setPasswordConfirmation] = useState({ value: '', error: '' })

  const onSignUpPressed = () => {
    // const nameError = nameValidator(name.value)
    // const emailError = emailValidator(email.value)
    // const passwordError = passwordValidator(password.value)
    // if (emailError || passwordError || nameError) {
    //   setName({ ...name, error: nameError })
    //   setEmail({ ...email, error: emailError })
    //   setPassword({ ...password, error: passwordError })
    //   return
    // }
    // navigation.reset({
    //   index: 0,
    //   routes: [{ name: 'Dashboard' }],
    // })
    // TODO: maybe would be cool if we changed the onChangeText to call a method to query the api to see if a username is available
    // and then can use HelperText to display if its available or not
    // TODO: HelperText to tell user if there is a specific problem with the password they are trying to use
    // TODO: verify both entered passwords are the same
  }

  return (
    <PaperProvider>
    <View>
      {/* <BackButton goBack={navigation.goBack} />
      <Logo />
      <Header>Create Account</Header> */}
      <View style={styles.container}>
        <UploadImage/>
      </View>
      <Text>Create Account</Text>
      <TextInput
        label="Username"
        returnKeyType="next"
        value={username.value}
        onChangeText={(text) => setUsername({ value: text, error: '' })}
        error={!!username.error}
        // errorText={username.error}
      />
      <TextInput
        label="Email"
        returnKeyType="next"
        value={email.value}
        onChangeText={(text) => setEmail({ value: text, error: '' })}
        error={!!email.error}
        // errorText={email.error}
        autoCapitalize="none"
        autoComplete="email"
        textContentType="emailAddress"
        keyboardType="email-address"
      />
      <TextInput
        label="Password"
        returnKeyType="done"
        value={password.value}
        onChangeText={(text) => setPassword({ value: text, error: '' })}
        error={!!password.error}
        // errorText={password.error}
        secureTextEntry
      />
      <TextInput
        label="Re-enter Password"
        returnKeyType="done"
        value={passwordConfirmation.value}
        onChangeText={(text) => setPasswordConfirmation({ value: text, error: '' })}
        error={!!passwordConfirmation.error}
        // errorText={passwordConfirmation.error}
        secureTextEntry
      />
      <Button
        mode="contained"
        onPress={onSignUpPressed}
        style={{ marginTop: 24 }}
      >
        Create Account
      </Button>
      <View style={styles.row}>
        <Text>Already have an account? </Text>
        <TouchableOpacity onPress={() => router.replace('LoginScreen')}>
          <Text style={styles.link}>Login</Text>
          {/* TODO: this route doesn't work */}
        </TouchableOpacity>
      </View>
    </View>
    </PaperProvider>
  )
}

const styles = StyleSheet.create({
  row: {
    flexDirection: 'row',
    marginTop: 4,
  },
  link: {
    fontWeight: 'bold',
    color: MD3DarkTheme.colors.primary,
  },
  container: {
    padding:50,
    backgroundColor: '#fff',
    alignItems: 'center',
    justifyContent: 'center',
  },
})