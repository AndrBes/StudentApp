import { request, useModel } from '@umijs/max';
import { Button, Form, Input, Modal, Select } from 'antd';
import { useState } from 'react';
import type { DraggableData, DraggableEvent } from 'react-draggable';
import Draggable from 'react-draggable';


export default (props: any) => {
    const [disabled, setDisabled] = useState(true);
    const { open, dataSource, bounds, draggleRef, groupOptions, setModalOpenState, setBounds, setDataSourceState } = useModel('useStudentModel')
  
    const onStudentAdd = (data: any) => {
      console.log(data)
      request('/api/Student/Add', { data, method: 'PUT'}).then((result: any[]) => {
      setModalOpenState(false)
  
      const newStudents = [...dataSource, result]
  
      setDataSourceState(newStudents)
    })
    };
    const onStart = (_event: DraggableEvent, uiData: DraggableData) => {
        const { clientWidth, clientHeight } = window.document.documentElement;
        const targetRect = draggleRef.current?.getBoundingClientRect();
        if (!targetRect) {
          return;
        }
        setBounds({
          left: -targetRect.left + uiData.x,
          right: clientWidth - (targetRect.right - uiData.x),
          top: -targetRect.top + uiData.y,
          bottom: clientHeight - (targetRect.bottom - uiData.y),
        });
      };

    return (
        <>
        <Modal
        title={
          <div
            style={{ width: '100%', cursor: 'move' }}
            onMouseOver={() => {
              if (disabled) {
                setDisabled(false);
              }
            }}
            onMouseOut={() => {
              setDisabled(true);
            }}
            // fix eslintjsx-a11y/mouse-events-have-key-events
            // https://github.com/jsx-eslint/eslint-plugin-jsx-a11y/blob/master/docs/rules/mouse-events-have-key-events.md
            onFocus={() => {}}
            onBlur={() => {}}
            // end
          >
            Добавление студента
          </div>
        }
        open={open}
        onOk={(e: React.MouseEvent<HTMLElement>) => {setModalOpenState(false)}}
        onCancel={(e: React.MouseEvent<HTMLElement>) => {setModalOpenState(false)}}
        modalRender={(modal) => (
          <Draggable
            disabled={disabled}
            bounds={bounds}
            nodeRef={draggleRef}
            onStart={(event, uiData) => onStart(event, uiData)}
          >
            <div ref={draggleRef}>{modal}</div>
          </Draggable>
        )}
      >
        <Form layout="inline"
          onFinish={onStudentAdd}
          >
          <Form.Item label="Группа" name = "groupId" className="input-inline">
            <Select
              allowClear
              options={groupOptions}
              style = {{width: '180px'}}
            >
            </Select>
          </Form.Item>
        <Form.Item label="Фамилия" name = "lastName" className="input-inline">
            <Input />
          </Form.Item>
          <Form.Item label="Имя" name = "firstName" className="input-inline">
            <Input />
          </Form.Item>
          <Form.Item label="Отчество" name = "midname" className="input-inline">
            <Input />
          </Form.Item>
          <Form.Item label="E-mail" name = "email" className="input-inline">
            <Input />
          </Form.Item>
          <Form.Item label="Пароль" name = "password" className="input-inline">
            <Input />
          </Form.Item>
          <Form.Item>
            <Button type="primary" htmlType='submit'>Добавить</Button>
          </Form.Item>
          </Form>
      </Modal>
        </>
    );
}