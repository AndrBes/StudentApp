import { Button, Form, message, Modal, Popconfirm, Select, Table, TableProps } from 'antd';
import { request, useModel } from '@umijs/max';
import { useEffect, useRef, useState } from 'react';
import Input from 'antd/es/input/Input';
import type { DraggableData, DraggableEvent } from 'react-draggable';
import Draggable from 'react-draggable';
import { useForm } from 'antd/es/form/Form';

export default function HomePage() {
  const { initialState } = useModel('@@initialState');
  
  const columns: TableProps<any>['columns'] = [
    {
      title: 'Id',
      dataIndex: 'studentId'
    },
    {
      title: 'Фамилия',
      dataIndex: 'lastName'
    },
    {
      title: 'Имя',
      dataIndex: 'firstName'
    },
    {
      title: 'Отчество',
      dataIndex: 'midname'
    },
    {
      title: 'E-mail',
      dataIndex: 'email'
    },
    {
      title: 'Группа',
      dataIndex: 'groupName'
    },
    // <DeleteOutlined />
    {
      title: 'Действия',
      dataIndex: 'action',
      render: (value, record, index) => <>
      <Popconfirm
        title="Удалить студента"
        description="Вы точно хотите удалить студента?"
        onConfirm={() => onRemove(record.studentId) }
        okText="Да"
        cancelText="Нет"
      >
              <a>Удалить</a>
      </Popconfirm>
      <Button onClick={() => onEdit(record.studentId)}>Редактировать</Button>
      </>
    }
  ]

  const [dataSource, setDataSource] = useState<any[]>([]);
  const [groupOptions, setGroupOptions] = useState<any[]>();
  const [open, setModalOpen] = useState(false);
  const [openEdit, setModalEditOpen] = useState(false);
  const [disabled, setDisabled] = useState(true);
  const [disabledEdit, setEditDisabled] = useState(true);
  const [bounds, setBounds] = useState({ left: 0, top: 0, bottom: 0, right: 0 });
  const draggleRef = useRef<HTMLDivElement>(null!);
  const [formEdit] = Form.useForm();

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

  useEffect(() => {

    onSearch({})

    request('/api/Group/GetAll', { data: {}, method: 'GET', headers: { 'Authorization': `Bearer ${localStorage.getItem('token')}` }}).then((result: any[]) => {

    const options = result.map((item: any) => ({ value: item.id, label: item.name}))
    
      setGroupOptions(options)
    });

    //localStorage.setItem('super_key', 'super_value')
    //localStorage.removeItem('super_key')

  }, [])

  const onSearch = (data: any) => {
    console.log(data)
    request('/api/Student/GetAll', { data: data, method: 'POST' }).then((result: any[]) => {
      setDataSource(result)
    });
  }
  const onRemove = (studentId: number) => {
    console.log('Удаляю студента с id = ' + studentId)
    request(`/api/Student/Delete?id=${studentId}`, { method: 'DELETE'}).then((result: any[]) => {
      const newDataSource = dataSource?.filter(item => item.studentId != studentId)
      setDataSource(newDataSource)
    });
  }
  const onStudentAdd = (data: any) => {
    console.log(data)
    request('/api/Student/Add', { data, method: 'PUT'}).then((result: any[]) => {
    setModalOpen(false)

    const newStudents = [...dataSource, result]

    setDataSource(newStudents)
  })
  }
  const onEdit = (studentId:number) => {
    setModalEditOpen(true)
    request(`/api/Student/Get?id=${studentId}`, { data: {}, method: 'GET'}).then((result: any[]) => {
      formEdit.setFieldsValue(result)
    });
  }
  const onStudentEdit = (data: any) => {
    console.log(data)
    request('/api/Student/Post', { data, method: 'POST'}).then((result: any) => {
    setModalEditOpen(false)
    message.success("Данные студента обновлены")

    const newStudents = dataSource.map((item: any) => {
      if(result.studentId == item.studentId) return result;
      return item
    })

    setDataSource(newStudents)
  })
  }
  

  return (
    <div>
          <div>{initialState?.login ? `Вы авторизованы как ${initialState.login}` : '123'}</div>
    <>
      <Button onClick={() => setModalOpen(true)}>Добавить студента</Button>
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
        onOk={(e: React.MouseEvent<HTMLElement>) => {setModalOpen(false)}}
        onCancel={(e: React.MouseEvent<HTMLElement>) => {setModalOpen(false)}}
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
      <Modal
        title={
          <div
            style={{ width: '100%', cursor: 'move' }}
            onMouseOver={() => {
              if (disabled) {
                setEditDisabled(false);
              }
            }}
            onMouseOut={() => {
              setEditDisabled(true);
            }}
            // fix eslintjsx-a11y/mouse-events-have-key-events
            // https://github.com/jsx-eslint/eslint-plugin-jsx-a11y/blob/master/docs/rules/mouse-events-have-key-events.md
            onFocus={() => {}}
            onBlur={() => {}}
            // end
          >
            Редактирование студента
          </div>
        }
        open={openEdit}
        onOk={(e: React.MouseEvent<HTMLElement>) => {setModalEditOpen(false)}}
        onCancel={(e: React.MouseEvent<HTMLElement>) => {setModalEditOpen(false)}}
        modalRender={(modal) => (
          <Draggable
            disabled={disabledEdit}
            bounds={bounds}
            nodeRef={draggleRef}
            onStart={(event, uiData) => onStart(event, uiData)}
          >
            <div ref={draggleRef}>{modal}</div>
          </Draggable>
        )}
      >
        <Form layout="inline"
          onFinish={onStudentEdit}
          form={formEdit}>
          <Form.Item label="Группа" name = "groupId" className="input-inline">
            <Select
              allowClear
              options={groupOptions}
              style = {{width: '180px'}}
            >
            </Select>
          </Form.Item>
          <Form.Item name = "studentId" hidden>
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
          <Form.Item label="Пароль" name = "password" className="input-inline" rules={[{ required: true, message: "Введите пароль"}]}>
            <Input />
          </Form.Item>
          <Form.Item>
            <Button type="primary" htmlType='submit'>Сохранить</Button>
          </Form.Item>
          </Form>
      </Modal>
    </>
    <Form
          layout="inline"
          onFinish={onSearch}
          // form={form}
          //initialValues={{ layout: formLayout }}
          //onValuesChange={onFormLayoutChange}
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
          <Form.Item>
            <Button type="primary" htmlType='submit'>Найти</Button>
          </Form.Item>
      </Form>
        <Table rowKey="studentId" dataSource={dataSource} columns={columns}></Table>
    </div>
  );
}
