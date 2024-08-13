import React, { useState } from 'react'
import { View } from 'react-native';
import { Link, router } from "expo-router";
import { PaperProvider, TextInput, Button, Text, MD3DarkTheme, Modal, Portal, Dialog, } from 'react-native-paper';
// import Background from '../components/Background'
// import BackButton from '../components/BackButton'
// import Logo from '../components/Logo'
// import Header from '../components/Header'
// import TextInput from '../components/TextInput'
// import Button from '../components/Button'
// import { emailValidator } from '../helpers/emailValidator'

export default function ResetPasswordScreen() {
  const [email, setEmail] = useState({ value: '', error: '' })
  const [visible, setVisible] = React.useState(false);

  const showDialog = () => setVisible(true);

  const hideDialog = () => setVisible(false);

  const sendResetPasswordEmail = () => {
    // const emailError = emailValidator(email.value)
    // if (emailError) {
    //   setEmail({ ...email, error: emailError })
    //   return
    // }
    

      

    // router.replace('LoginScreen')
    // TODO: tell the user that something happened, maybe pop up a toast that they should get an email or something
  }

  return (
    <PaperProvider>
    <View>
      {/* <BackButton goBack={navigation.goBack} />
      <Logo />
      <Header>Restore Password</Header> */}
      <Text>Reset Password</Text>
      <Text>Enter your email address or username and we’ll send you a link to reset your password</Text>
      {/* TODO: work with username or email */}
      <TextInput
        label="Email or username"
        returnKeyType="done"
        value={email.value}
        onChangeText={(text) => setEmail({ value: text, error: '' })}
        error={!!email.error}
        // errorText={email.error}
        autoCapitalize="none"
        autoComplete="email"
        textContentType="emailAddress"
        keyboardType="email-address"
      />
      <Button
        mode="contained"
        onPress={
            () => { sendResetPasswordEmail(); showDialog(); }
           }
        style={{ marginTop: 16 }}
      >
        Reset Password
      </Button>
      <Portal>
      <Dialog visible={visible} onDismiss={hideDialog}>
            <Dialog.Content>
              <Text variant="bodyLarge">An email with a link to reset your password was sent to the email address associated with your account</Text>
            </Dialog.Content>
            <Dialog.Actions>
              <Button onPress={hideDialog}>Done</Button>
            </Dialog.Actions>
          </Dialog>
      </Portal>
    </View>
    </PaperProvider>
  )
}