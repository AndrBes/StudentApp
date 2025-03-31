import { Button, Form, message, Modal, Popconfirm, Select, Table, TableProps } from 'antd';
import { request, useModel } from '@umijs/max';
import { useEffect, useRef, useState } from 'react';
import Input from 'antd/es/input/Input';
import type { DraggableData, DraggableEvent } from 'react-draggable';
import Draggable from 'react-draggable';
import { useForm } from 'antd/es/form/Form';

export default function HomePage() {
  const { setInitialState } = useModel('@@initialState');
  


  const loginHandler = (data: any) => {
    request('/api/Auth/Login', { method: 'POST', data}).then((result: any) => {
        if(result.status != 0) {
          message.error("Авторизация не удалась")
        }
        else {
          setInitialState({ login: result.login})
          localStorage.setItem('token', result.token)
          message.success("Авторизация выполнена")
        }
      });
  }

  return (
        <Form layout="inline"
          onFinish={loginHandler}>
        <Form.Item label="Логин" name = "login" className="input-inline">
            <Input />
          </Form.Item>
          <Form.Item label="Пароль" name = "password" className="input-inline">
            <Input />
          </Form.Item>
          <Form.Item>
            <Button type="primary" htmlType='submit'>Войти</Button>
          </Form.Item>
          </Form>
  );
}
