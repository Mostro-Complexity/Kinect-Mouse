% function: simulating the process of EKF  
q = 0.1;          % 过程标准差  
r = 0.2;           % 测量标准差  
% eye函数产生单位矩阵  
Q = q^2*eye(1);   % 过程方差  
R = r^2;               % 测量值的方差  
  
f = @(x)(x);  % 状态方程  
h = @(x)(x);                          % 测量方程  
s = 1;                                            % 初始状态  
  
% 初始化状态  
x = s+q*randn;   
% eye返回单位矩阵  
P = eye(1);  
% 最优估计值  
xV = zeros(1,500);  

% 状态测量值  
zV = s + r * randn(1,500);  
  
for k = 1:size(zV,2)  
  %  z = s + r * randn;  
    % 状态测量值  说白了就一随机值
 %   zV(:,k) = z;  
      
    % 计算f的雅可比矩阵，其中x1对应黄金公式line2  
    [x1,A] = jaccsd(f,x);  
    % 过程方差预测，对应line3  
    P = A*P*A'+Q;  
    % 计算h的雅可比矩阵  
    [z1,H] = jaccsd(h,x1);  
      
    % 卡尔曼增益，对应line4  
    % inv返回逆矩阵  
    K = P*H'*inv(H*P*H'+R);  
    % 状态EKF估计值，对应line5  
    x = x1+K*(zV(:,k)-z1);  
    % EKF方差，对应line6  
    P = P-K*H*P;  
      
    % save  
    xV(:,k) = x;  
    % update process   
    s = f(s) + q*randn;  
end

figure();
 

% 画出最优估计值  
plot(xV,'b-');  
hold on;  

% 画出状态测量值  
plot(zV,'r-');  
hold on;  

legend( 'EKF最优估计估计值','状态测量值');  
xlabel('时间(分钟)');  
% 把数值转换成字符串， 转换后可以使用fprintf或disp函数进行输出。  
t = ['状态 ',num2str(k)] ;  
ylabel(t);  
