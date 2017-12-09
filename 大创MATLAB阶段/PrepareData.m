function Z=PrepareData(X1,T, Obj, Meter )
    % 状态X=[x1,x2]  位置x1，速度x2
    % 状态方程 X(t) = phi*X(t-1)+ B*u(t-1)+gamma*e1(t-1);
    %      phi=[1 1; 0,1] 
    %      B=[1^2/2; T]    u(t)加速度
    %      gamma=B;        e1(t)系统噪音（方差Q） 
    % 测量方程 Z(t)=H*X(t)+e2(t);
    %         H=[1;0];
    %         e2(t)测量噪音（方差R）

    % 1 模拟Obj的运行过程，存入X
	e1=normrnd(0, Obj.Q, 1,T);  % 系统噪音
    X(:,1)=X1;
    for t=2:T
        X(:,t)=Obj.phi*X(:,t-1)+Obj.B*Obj.us(t-1)+Obj.gamma*e1(t-1);
    end
    % 2 模拟观察记录,存入Z
    e2=normrnd(0,Meter.R,1,T); % 观察噪音
    Z=X(1,:)+e2;
end
