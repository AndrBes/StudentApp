import { Link, Outlet, useModel } from 'umi';
import  './index.less';
import React, { useState } from 'react';
import {
  MenuFoldOutlined,
  MenuUnfoldOutlined,
  UploadOutlined,
  UserOutlined,
  VideoCameraOutlined,
} from '@ant-design/icons';
import { Button, Layout, Menu, theme } from 'antd';

const { Header, Sider, Content } = Layout;
const App: React.FC = () => {
  const { initialState, setInitialState} = useModel('@@initialState');
  const [collapsed, setCollapsed] = useState(false);
  const onExit = () => {
    localStorage.removeItem('token')
    setInitialState({login: undefined})
  }
  const {
    token: { colorBgContainer, borderRadiusLG },
  } = theme.useToken();


const menuItems = [
  {
    key: 'home',
    icon: <UserOutlined />,
    label: <Link to="/">Home</Link>,
  },
  {
    key: 'auth',
    icon: <UserOutlined />,
    label: initialState?.login ? <a onClick={onExit}>Exit</a> : <Link to="/auth">Auth</Link>,
  },
  {
    key: 'docs',
    icon: <VideoCameraOutlined />,
    label: <Link to="/docs">Docs</Link>,
  },
  {
    key: 'user',
    icon: <VideoCameraOutlined />,
    label: <Link to="/user">User</Link>,
  },
  {
    key: 'github',
    icon: <UploadOutlined />,
    label: <a href="https://github.com/">Github</a>,
  },
]  
  return (
    <Layout style = {{ minHeight:'100vh' }}>
      <Sider trigger={null} collapsible collapsed={collapsed}>
        <div className="demo-logo-vertical" />
        <Menu
          theme="dark"
          mode="inline"
          defaultSelectedKeys={['1']}
          items={menuItems}
        />
      </Sider>
      <Layout>
        <Header style={{ padding: 0, background: colorBgContainer, alignItems: 'center' }}>
          <Button
            type="text"
            icon={collapsed ? <MenuUnfoldOutlined /> : <MenuFoldOutlined />}
            onClick={() => setCollapsed(!collapsed)}
            style={{
              fontSize: '16px',
              width: 64,
              height: 64,
            }}
          />
        </Header>
        <Content
          style={{
            margin: '24px 16px',
            padding: 24,
            minHeight: 280,
            background: colorBgContainer,
            borderRadius: borderRadiusLG,
          }}
        >
          <Outlet />
        </Content>
      </Layout>
    </Layout>
  );
    // <div className={styles.navs}>
    //   <ul>
    //     <li>
    //       <Link to="/">Home</Link>
    //     </li>
    //     <li>
    //       <Link to="/docs">Docs</Link>
    //     </li>
    //     <li>
    //       <a href="https://github.com/umijs/umi">Github</a>
    //     </li>
    //   </ul>
    //   <Outlet />
    // </div>
}

export default App;