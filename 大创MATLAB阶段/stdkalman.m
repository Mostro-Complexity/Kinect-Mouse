function Ks = stdkalman(Z,T, Obj, Meter)
%  模型的基本特性1： 状态转移Obj.phi  Obj.B  Obj.gamma
%  模型的基本特性2： 加速度序列Obj.us  当前加速度Obj.u  
%  模型的基本特性3： 加速度的噪音方差Obj.Q
%  测量工具的特性：  测量系数Meter.H    测量误差的方差Meter.R  
%  卡尔曼滤波参数：  状态估计K.x   方差估计矩阵K.p   卡尔曼增益K.K
    % 1 初始状态
    Ks=[];
    K.x=[Z(1);(Z(2)-Z(1))];    K.p=1000*eye(2);
    % 2 迭代
    for t=2:T
        Obj.u=Obj.us(:,t);  % 当前控制量
        newK = stdkalman1(Z(t), Obj, Meter, K); % 当前测量值Z(t)
        Ks=[Ks newK];
    end
end

function newK = stdkalman1(z, Obj, Meter,  K)
    % 1 先验估计的状态值
    X=Obj.phi*K.x+Obj.B*Obj.u;
    P=Obj.phi*K.p*Obj.phi'+Obj.gamma*Obj.Q*Obj.gamma';  
    % 2 计算新的滤波参数
    newK.K=P*Meter.H'* inv( Meter.H*P*Meter.H'+Meter.R );
    newK.X=X+newK.K*( z-Meter.H*X );
    n=length(X);
    newK.P=(eye(n)-newK.K*Meter.H)*P*(eye(n)-newK.K*Meter.H)'+newK.K*Meter.R*newK.K';
end
